<script>
    import { createEventDispatcher } from "svelte";
    import {
        deleteUnitAsync,
        updateResourceAmountAsync,
        addResourceAsync,
        getPlansContainingAsync,
        deleteResourceFromUnitAsync,
    } from "./unit-provider";
    import { getResourcesAsync } from "../resources/resource-provider";

    import ModalComponent from "../ModalComponent.svelte";
    import Multiselect from "svelte-multiselect/src/Multiselect.svelte";

    const dispatch = createEventDispatcher();

    export let items;

    //fill select
    let optionsResources = [];
    const fillResourceSelect = async function () {
        optionsResources = await getResourcesAsync();
    };
    fillResourceSelect();

    //remove unit
    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deleteUnitAsync(selectedItem);
            // location.href = "#/resources/";
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

    //edit amount of resource in a unit
    let amountToEdit = 1;
    let selectedItemEdit = null;
    const editAmountResource = async function (parentItem) {
        if (selectedItemEdit) {
            await updateResourceAmountAsync(
                parentItem.name,
                selectedItemEdit.resource.id,
                amountToEdit
            );
            dispatch("needsRefresh");
            selectedItemEdit = null;
            await modalEditUnit.hide();
        }
    };
    let modalEditUnit;
    const showModalEditUnit = async (item) => {
        selectedItemEdit = item;
        await modalEditUnit.show();
    };

    //add resource to unit
    let selectedResource;
    let resourceAmountInput = 1;
    const addResourceToList = async function (parentItem) {
        await addResourceAsync(
            parentItem.name,
            selectedResource.id,
            resourceAmountInput
        );
        dispatch("needsRefresh");
        await modalAddResource.hide();
    };
    let modalAddResource;
    const showModalAddResource = async () => {
        await modalAddResource.show();
    };

    //remove resource from unit
    let selectedRes = null;
    const removeResource = async function (parentItem) {
        if (selectedRes) {
            console.log("mravnecnik", selectedRes);
            await deleteResourceFromUnitAsync(
                parentItem.name,
                selectedRes.resource.id
            );
            selectedRes = null;
            dispatch("needsRefresh");
            modalRemoveRes.hide();
        }
    };
    let modalRemoveRes;
    const showModalRemoveRes = (item) => {
        selectedRes = item;
        modalRemoveRes.show();
    };

    //get data about units containing resource
    let plans = [];
    const getPlanDataAsync = async function (items) {
        plans = await getPlansContainingAsync(items[0].name);
    };
    $: getPlanDataAsync(items);
</script>

<form on:submit|stopPropagation>
    <div class="form-group row">
        <label for="itemName" class="col-form-label pl-3 pr-3">Name: </label>
        <div>
            <input
                type="text"
                readonly
                class="form-control-plaintext"
                id="itemName"
                value={items[0].name}
                style="color:black;"
            />
        </div>
        <!-- adding items -->
        <div class="ml-auto" style="padding-right: 28px;">
            <button
                on:click={() => {
                    showModalAddResource();
                    resourceAmountInput = 0;
                }}
                class="btn btn-sm btn-success rounded-0"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Add"
                style="text-align:right;"
            >
                <i class="fa fa-sm fa-plus" style="padding: 0.5rem, 0.7rem;" />
            </button>

            <ModalComponent bind:this={modalAddResource}>
                <h1 style="text-align:center;">Add resource</h1>

                <h4>Resource Name:</h4>
                <Multiselect
                    small
                    bind:value={selectedResource}
                    options={optionsResources}
                    multiple={false}
                    closeOnSelect={true}
                    clearOnSelect={false}
                    placeholder="Select item to add"
                    trackBy="name"
                    label="name"
                />

                <h4>Resource Amount</h4>
                <input
                    type="number"
                    class="form-control"
                    bind:value={resourceAmountInput}
                    min="1"
                />

                <button
                    style="position:absolute;bottom: 1em;right:5%"
                    on:click={async () => {
                        await addResourceToList(items[0]);
                    }}>Add</button
                >
            </ModalComponent>

            <button
                on:click={() => showModal(items[0].name)}
                class="btn btn-sm btn-danger rounded-0"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Delete"
                style="text-align:right;"
            >
                <i class="fa fa-sm fa-trash" style="padding: 0.5rem, 0.7rem;" />
            </button>
            <ModalComponent bind:this={modalcomponent}>
                <h1 style="text-align:center;">Are you sure?</h1>
                <button
                    style="position:absolute;bottom: 1em;right:5%"
                    on:click={async () => {
                        await remove();
                    }}>Delete</button
                >
            </ModalComponent>
        </div>
    </div>
</form>

<!-- list of resources inside -->
<h3>Resources</h3>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col" class="auto-width">#</th>
            <th scope="col" class="auto-width">Actions</th>
            <th scope="col" class="auto-width">Name</th>
            <th scope="col">Amount</th>
        </tr>
    </thead>
    <tbody>
        {#each items[0].resourceList as item, i}
            <tr>
                <td class="auto-width">{i + 1}</td>
                <td class="auto-width">
                    <button
                        on:click={() => showModalEditUnit(item)}
                        class="btn btn-sm btn-dark rounded-0"
                        type="button"
                        data-toggle="tooltip"
                        data-placement="top"
                        title="Edit"
                        style="text-align:right;"
                    >
                        <i
                            class="fa fa-sm fa-edit"
                            style="padding: 0.5rem, 0.7rem;"
                        />
                    </button>
                    <ModalComponent bind:this={modalEditUnit}>
                        <h1 style="text-align:center;">Update amount</h1>
                        <input
                            type="number"
                            bind:value={amountToEdit}
                            style="margin-top: 60px;"
                            min="1"
                        />
                        <button
                            style="position:absolute;bottom: 1em;right:5%"
                            on:click={async () => {
                                await editAmountResource(items[0]);
                            }}>Update</button
                        >
                    </ModalComponent>
                    <button
                        on:click={() => showModalRemoveRes(item)}
                        class="btn btn-sm btn-danger rounded-0"
                        type="button"
                        data-toggle="tooltip"
                        data-placement="top"
                        title="Delete"
                        style="text-align:right;"
                    >
                        <i
                            class="fa fa-sm fa-trash"
                            style="padding: 0.5rem, 0.7rem;"
                        />
                    </button>
                    <ModalComponent bind:this={modalRemoveRes}>
                        <h1 style="text-align:center;">Are you sure?</h1>
                        <button
                            style="position:absolute;bottom: 1em;right:5%"
                            on:click={async () => {
                                await removeResource(items[0]);
                            }}>Delete</button
                        >
                    </ModalComponent>
                </td>

                <td class="auto-width">
                    <div class="text-truncate">
                        <a href="#/resources/{item.resource.name}"
                            >{item.resource.name}</a
                        >
                    </div>
                </td>
                <td>{item.amount}</td>
            </tr>
        {/each}
    </tbody>
</table>

<!-- plans containing this unit -->
<h3>Battle Plans Containing</h3>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col" class="auto-width">#</th>
            <th scope="col">Name</th>
        </tr>
    </thead>
    <tbody>
        {#each plans as item, i}
            <tr>
                <td scope="row">{i + 1}</td>
                <td><a href="#/planBattle/{item.name}"> {item.name}</a></td>
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
