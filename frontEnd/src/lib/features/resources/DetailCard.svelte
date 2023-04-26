<script>
    import { createEventDispatcher } from "svelte";
    import { deleteResourceAsync } from "./resource-provider";
    import {
        updateRequirementAmountAsync,
        addRequirementAsync,
        getUnitsContainingAsync,
        deleteRequirementFromResourceAsync,
    } from "./resource-provider";
    import { getRequirementsAsync } from "../requirements/requirement-provider";
    import Multiselect from "svelte-multiselect/src/Multiselect.svelte";
    import ModalComponent from "../ModalComponent.svelte";

    const dispatch = createEventDispatcher();

    export let items;

    //fill select
    let optionsRequirements = [];
    const fillRequirementSelect = async function () {
        optionsRequirements = await getRequirementsAsync();
    };
    fillRequirementSelect();

    //remove resource
    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deleteResourceAsync(selectedItem);
            selectedItem = null;
            location.href = "#/resources/";
            modalcomponent.hide();
        }
    };
    let modalcomponent;
    const showModal = (item) => {
        selectedItem = item;
        modalcomponent.show();
    };

    //remove requirement from resource
    let selectedReq = null;
    const removeRequirement = async function (parentItem) {
        if (selectedReq) {
            console.log("mravnecnik", selectedReq);
            await deleteRequirementFromResourceAsync(
                parentItem.name,
                selectedReq.requirement.id
            );
            selectedReq = null;
            dispatch("needsRefresh");
            modalRemoveReq.hide();
        }
    };
    let modalRemoveReq;
    const showModalRemoveReq = (item) => {
        selectedReq = item;
        modalRemoveReq.show();
    };

    //edit amount of item
    let amountToEdit = 1;
    let selectedItemEdit = null;
    const editAmount = async function (parentItem) {
        if (selectedItemEdit) {
            await updateRequirementAmountAsync(
                parentItem.name,
                selectedItemEdit.requirement.id,
                amountToEdit
            );
            dispatch("needsRefresh");
            selectedItemEdit = null;
            modalEdit.hide();
        }
    };

    let modalEdit;
    const showModalEdit = async (item) => {
        selectedItemEdit = item;
        await modalEdit.show();
    };

    //add item to list
    let selectedRequirement;
    let requirementAmountInput = 1;
    const addRequirementToList = async function (parentItem) {
        await addRequirementAsync(
            parentItem.name,
            selectedRequirement.id,
            requirementAmountInput
        );
        dispatch("needsRefresh");
        await modalAdd.hide();
    };
    let modalAdd;
    const showModalAdd = async () => {
        await modalAdd.show();
    };

    //get data about units containing resource
    let units = [];
    const getUnitDataAsync = async function (items) {
        units = await getUnitsContainingAsync(items[0].name);
    };
    $: getUnitDataAsync(items);
</script>

<form on:submit|stopPropagation>
    <div class="form-group row">
        <!-- adding items -->
        <div class="col-sm-2">
            <button
                on:click={() => {
                    showModalAdd();
                    requirementAmountInput = 0;
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

            <ModalComponent bind:this={modalAdd}>
                <h1 style="text-align:center;">Add requirement</h1>

                <h4>Requirement Name:</h4>
                <Multiselect
                    small
                    bind:value={selectedRequirement}
                    options={optionsRequirements}
                    multiple={false}
                    closeOnSelect={true}
                    clearOnSelect={false}
                    placeholder="Select item to add"
                    trackBy="name"
                    label="name"
                />

                <h4>Requirement Amount</h4>
                <input
                    type="number"
                    class="form-control"
                    bind:value={requirementAmountInput}
                    min="1"
                />

                <button
                    style="position:absolute;bottom: 1em;right:5%"
                    on:click={async () => {
                        await addRequirementToList(items[0]);
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
        <label for="itemName" class="col-sm-2 col-form-label">Name: </label>
        <div class="col-sm-6">
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
<!-- list of requirements inside -->
<h3>Requirements</h3>
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
        {#each items[0].requirementList as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td>
                    <button
                        on:click={() => showModalEdit(item)}
                        class="btn btn-sm btn-dark rounded-0"
                        type="button"
                        data-toggle="tooltip"
                        data-placement="top"
                        title="Edit"
                    >
                        <i
                            class="fa fa-sm fa-edit"
                            style="padding: 0.5rem, 0.7rem;"
                        />
                    </button>
                    <ModalComponent bind:this={modalEdit}>
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
                                await editAmount(items[0]);
                            }}>Update</button
                        >
                    </ModalComponent>
                    <button
                        on:click={() => showModalRemoveReq(item)}
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
                    <ModalComponent bind:this={modalRemoveReq}>
                        <h1 style="text-align:center;">Are you sure?</h1>
                        <button
                            style="position:absolute;bottom: 1em;right:5%"
                            on:click={async () => {
                                await removeRequirement(items[0]);
                            }}>Delete</button
                        >
                    </ModalComponent>
                </td>
                <td>
                    <a href="#/requirements/{item.requirement.name}"
                        >{item.requirement.name}</a
                    >
                </td>
                <td>{item.amount}</td>
            </tr>
        {/each}
    </tbody>
</table>

<!-- units containing this resource -->
<h3>Units Containing</h3>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
        </tr>
    </thead>
    <tbody>
        {#each units as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td><a href="#/units/{item.name}"> {item.name}</a></td>
            </tr>
        {/each}
    </tbody>
</table>
