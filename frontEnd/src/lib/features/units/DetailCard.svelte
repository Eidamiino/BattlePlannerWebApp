<script>
    export let items;
    console.log(items);
    import ModalComponent from "../ModalComponent.svelte";
    import { deleteUnitAsync } from "./unit-provider";

    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deleteUnitAsync(selectedItem);
            selectedItem = null;
        }
    };
    let modalcomponent;
    const showModal = (item) => {
        selectedItem = item;
        modalcomponent.show();
    };
</script>

<form on:submit|preventDefault|stopPropagation>
    <div class="form-group row">
        <label for="itemName" class="col-sm-2 col-form-label">Name: </label>
        <div class="col-sm-8">
            <input
                type="text"
                readonly
                class="form-control-plaintext"
                id="itemName"
                value={items[0].name}
                style="color:black;"
            />
        </div>
        <div class="col-sm-2">
            <button
                on:click={() => showModal(items[0].name)}
                class="btn btn-danger rounded-0"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Delete"
                style="text-align:right;"
            >
                <i class="fa fa-trash" style="padding: 0.5rem, 0.7rem;" />
            </button>
            <ModalComponent bind:this={modalcomponent}>
                <h1 style="text-align:center;">Are you sure?</h1>
                <button
                    style="position:absolute;bottom: 1em;left:40%"
                    on:click={async () => {
                        console.log("removing:" + selectedItem);
                        await remove();
                    }}>Delete</button
                >
            </ModalComponent>
        </div>
    </div>
</form>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
            <th scope="col">Amount</th>
        </tr>
    </thead>
    <tbody>
        {#each items[0].resourceList as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td
                    ><a href="#/resources/{item.resource.name}"
                        >{item.resource.name}</a
                    ></td
                >
                <td>{item.amount}</td>
            </tr>
        {/each}
    </tbody>
</table>
