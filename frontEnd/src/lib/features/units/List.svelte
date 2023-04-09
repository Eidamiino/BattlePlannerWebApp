<!-- <script>
    export let Resources;
</script>

{#each Resources as item}
    <tr
        ><td>{JSON.stringify(item.name)}</td><td
            >{JSON.stringify(item.requirementList[0].requirement.name)}</td
        ><td>{JSON.stringify(item.requirementList[0].amount)}</td></tr
    >
{/each} -->
<script>
    export let items;
    import {
        // deleteRequirementAsync,
        getUnitsAsync,
        deleteUnitAsync,
    } from "./unit-provider";
    import ModalComponent from "../ModalComponent.svelte";
    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deleteUnitAsync(selectedItem.name);
            await getUnitsAsync();
            selectedItem = null;
        }
    };
    let modalcomponent;
    const showModal = (item) => {
        selectedItem = item;
        modalcomponent.show();
    };
</script>

{#each items as item}
    <tr>
        <td class="title"><a href="#/units/{item.name}">{item.name}</a></td>
        <button
            on:click={() => showModal(item)}
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
