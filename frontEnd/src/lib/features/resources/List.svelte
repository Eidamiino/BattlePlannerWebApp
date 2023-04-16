<script>
    import {
        getResourcesAsync,
        deleteResourceAsync,
    } from "./resource-provider";
    import ModalComponent from "../ModalComponent.svelte";

    export let items;

    //remove item from list
    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deleteResourceAsync(selectedItem.name);
            location.href = "#/resources/";
            selectedItem = null;
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
        <td class="title"><a href="#/resources/{item.name}">{item.name}</a></td>
        <button
            on:click={async () => await showModal(item)}
            class="btn btn-danger rounded-0"
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
                style="position:absolute;bottom: 1em;left:40%"
                on:click={async () => {
                    console.log("removing:" + selectedItem.name);
                    await remove();
                }}>Delete</button
            >
        </ModalComponent>
    </tr>
{/each}
