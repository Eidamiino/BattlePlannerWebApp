<script>
    import { createEventDispatcher } from "svelte";
    import { deletePlanAsync, getPlansAsync } from "./battlePlan-provider";
    import ModalComponent from "../ModalComponent.svelte";

    const dispatch = createEventDispatcher();

    export let items;

    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deletePlanAsync(selectedItem.name);
            selectedItem = null;
            modalcomponent.hide();
            dispatch("needsRefreshList");
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
        <button
            on:click={() => showModal(item)}
            class="btn btn-danger rounded-0 btn-sm"
            type="button"
            data-toggle="tooltip"
            data-placement="top"
            title="Delete"
        >
            <i class="fa fa-trash" />
        </button>
        <ModalComponent bind:this={modalcomponent}>
            <h1 style="text-align:center;">Are you sure?</h1>
            <button
                style="position:absolute;bottom: 1em;right:5%"
                on:click={() => {
                    remove();
                }}>Delete</button
            >
        </ModalComponent>
        <td class="title" style="border:0;"
            ><a href="#/planBattle/{item.name}">{item.name}</a></td
        >
    </tr>
{/each}

<!-- on:click={async () => {
                    dispatch(
                        "showDetail",
                        await getRequirementAsync(item.name)
                    );
                }}
             -->

<!-- on:click={async () => {
                await deleteRequirementAsync(item.name);
            }} -->
