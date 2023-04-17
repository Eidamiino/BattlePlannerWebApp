<script>
    import { createEventDispatcher } from "svelte";
    import {
        deleteUnitAsync,
        updateResourceAmountAsync,
        addResourceAsync,
        getUnitsAsync,
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
            selectedItem = null;
            dispatch("needsRefresh");
            modalcomponent.hide();
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
</script>

<form on:submit|stopPropagation>
    <div class="form-group row">
        <!-- adding items -->
        <div class="col-sm-2">
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
                    style="position:absolute;bottom: 1em;left:40%"
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
                    style="position:absolute;bottom: 1em;left:40%"
                    on:click={async () => {
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
                value={items[0].name}
                style="color:black;"
            />
        </div>
    </div>
</form>

<!-- list of resources inside -->
<h3>Resources</h3>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Actions</th>
            <th scope="col">Name</th>
            <th scope="col">Amount</th>
        </tr>
    </thead>
    <tbody>
        {#each items[0].resourceList as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td>
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
                            style="position:absolute;bottom: 1em;left:40%"
                            on:click={async () => {
                                await editAmountResource(items[0]);
                            }}>Update</button
                        >
                    </ModalComponent>
                </td>

                <td>
                    <a href="#/resources/{item.resource.name}"
                        >{item.resource.name}</a
                    >
                </td>
                <td>{item.amount}</td>
            </tr>
        {/each}
    </tbody>
</table>
