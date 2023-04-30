<script>
    import { createEventDispatcher } from "svelte";
    import {
        getResourcesAsync,
        deleteResourceAsync,
    } from "./resource-provider";
    import ModalComponent from "../ModalComponent.svelte";

    const dispatch = createEventDispatcher();

    export let items;

    //remove item from list
    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deleteResourceAsync(selectedItem.name);
            // location.href = "#/resources/";
            selectedItem = null;
            dispatch("needsRefreshList");
            await modalcomponent.hide();
        }
    };
    let modalcomponent;
    const showModal = async (item) => {
        selectedItem = item;
        await modalcomponent.show();
    };
</script>

{#each items as item}
    <tr>
        <button
            on:click={async () => await showModal(item)}
            class="btn btn-danger rounded-0 btn-sm"
            type="button"
            data-toggle="tooltip"
            data-placement="top"
            title="Delete"
        >
            <i class="fa fa-trash" style="padding: 0.5rem, 0.7rem;" />
        </button>
        <ModalComponent bind:this={modalcomponent}>
            <h1 style="text-align:center;">Are you sure?</h1>
            <button
                style="position:absolute;bottom: 1em;right:5%"
                on:click={async () => {
                    console.log("removing:" + selectedItem.name);
                    await remove();
                }}>Delete</button
            >
        </ModalComponent>
        <td class="title" style="border:0;"
            ><a href="#/resources/{item.name}">{item.name}</a></td
        >
    </tr>
{/each}
