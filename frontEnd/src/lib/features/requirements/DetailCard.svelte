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
            selectedItem = null;
            dispatch("needsRefreshGoHome");
            await modalcomponent.hide();
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


    <div class="form-group row">
        <label for="itemName" class="col-form-label pl-3 pr-3">Name: </label>
        <div>
            <input
                type="text"
                readonly
                class="form-control-plaintext"
                id="itemName"
                value={items.name}
                style="color:black;"
            />
        </div>
        <!-- removing -->
        <div class="ml-auto" style="padding-right: 28px;">
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
    </div>


<!-- resources containing this requirement -->
<h3>Resources Containing</h3>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col" class="auto-width">#</th>
            <th scope="col">Name</th>
        </tr>
    </thead>
    <tbody>
        {#each resources as item, i}
            <tr>
                <td class="auto-width">{i + 1}</td>
                <td><a href="#/resources/{item.name}"> {item.name}</a></td>
            </tr>
        {/each}
    </tbody>
</table>

<style>
    .auto-width {
        width: 1px;
        white-space: nowrap;
    }
</style>
