<script>
    import { createEventDispatcher } from "svelte";
    import { afterUpdate } from "svelte";
    import {
        deleteRequirementAsync,
        getResourcesContainingAsync,
    } from "./requirement-provider";
    import ModalComponent from "../ModalComponent.svelte";

    const dispatch = createEventDispatcher();

    export let items;

    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deleteRequirementAsync(selectedItem);
            dispatch("needsRefresh");
            selectedItem = null;
            modalcomponent.hide();
        }
    };
    let modalcomponent;
    const showModal = (item) => {
        selectedItem = item;
        modalcomponent.show();
    };

    let resources = [];
    const getResourceDataAsync = async function (items) {
        resources = await getResourcesContainingAsync(items.name);
    };
    $: getResourceDataAsync(items);
</script>

<form on:submit|preventDefault|stopPropagation>
    <div class="form-group row">
        <div class="col-sm-2">
            <button
                on:click={() => showModal(items.name)}
                class="btn btn-danger btn-sm rounded-0"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Delete"
                style="text-align:right;"
            >
                <i class="fa fa-trash fa-sm" style="padding: 0.5rem, 0.7rem;" />
            </button>
            <ModalComponent bind:this={modalcomponent}>
                <h1 style="text-align:center;">Are you sure?</h1>
                <button
                    style="position:absolute;bottom: 1em;right:5%"
                    on:click={async () => {
                        console.log("removing:" + selectedItem);
                        await remove();
                    }}>Delete</button
                >
            </ModalComponent>
        </div>
        <label for="itemName" class="col-sm-2 col-form-label">Name: </label>
        <div class="col-sm-8">
            <input
                type="text"
                readonly
                class="form-control-plaintext"
                id="itemName"
                value={items.name}
                style="color:black;"
            />
        </div>
    </div>
</form>

<!-- resources containing this requirement -->
<h3>Resources Containing</h3>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
        </tr>
    </thead>
    <tbody>
        {#each resources as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td><a href="#/resources/{item.name}"> {item.name}</a></td>
            </tr>
        {/each}
    </tbody>
</table>
