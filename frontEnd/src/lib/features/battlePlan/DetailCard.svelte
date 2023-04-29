<script>
    import { createEventDispatcher } from "svelte";
    import {
        addUnitAsync,
        deletePlanAsync,
        getSummaryAsync,
        deleteUnitFromPlanAsync,
    } from "./battlePlan-provider";
    import { getUnitsAsync } from "../units/unit-provider";

    import Multiselect from "svelte-multiselect/src/Multiselect.svelte";
    import ModalComponent from "../ModalComponent.svelte";

    const dispatch = createEventDispatcher();

    export let items;

    //fill select
    let optionsUnits = [];
    const fillUnitSelect = async function () {
        optionsUnits = await getUnitsAsync();
    };
    fillUnitSelect();

    //get summary
    let summaryItems = [];
    const fillSummaryAsync = async function (items) {
        summaryItems = await getSummaryAsync(items[0].name);
    };
    $: fillSummaryAsync(items);

    //remove plan
    let selectedItem;
    const remove = async function () {
        if (selectedItem) {
            await deletePlanAsync(selectedItem);
            location.href = "#/planBattle/";
            selectedItem = null;
            await modalcomponent.hide();
        }
    };
    let modalcomponent;
    const showModal = async (item) => {
        selectedItem = item;
        await modalcomponent.show();
    };
    //adding units
    let selectedUnit;
    const addUnitToList = async function (parentItem) {
        await addUnitAsync(parentItem.name, selectedUnit.id);
        location.href = "#/planBattle/" + items[0].name;
        await modalAdd.hide();
    };
    let modalAdd;
    const showModalAddPlan = async () => {
        await modalAdd.show();
    };
    //remove unit from plan
    let selectedUnitRemove = null;
    const removeResource = async function (parentItem) {
        if (selectedUnitRemove) {
            await deleteUnitFromPlanAsync(
                parentItem.name,
                selectedUnitRemove.id
            );
            selectedUnitRemove = null;
            dispatch("needsRefresh");
            modalRemoveUnit.hide();
        }
    };
    let modalRemoveUnit;
    const showModalRemoveUnit = (item) => {
        selectedUnitRemove = item;
        modalRemoveUnit.show();
    };
</script>

<form on:submit|stopPropagation>
    <div class="form-group row" style="padding-left:28px;">
        <label for="itemName" class="col-form-label">Name: </label>
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
        <!-- adding units -->
        <div class="ml-auto" style="padding-right: 28px;">
            <button
                on:click={() => {
                    showModalAddPlan();
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
                <h1 style="text-align:center;">Add Unit</h1>

                <h4>Unit Name:</h4>
                <Multiselect
                    small
                    bind:value={selectedUnit}
                    options={optionsUnits}
                    multiple={false}
                    closeOnSelect={true}
                    clearOnSelect={false}
                    placeholder="Select item to add"
                    trackBy="name"
                    label="name"
                />

                <button
                    style="position:absolute;bottom: 1em;right:5%"
                    on:click={async () => {
                        await addUnitToList(items[0]);
                    }}>Add</button
                >
            </ModalComponent>
            <!-- removing plans -->
            <button
                on:click={() => showModal(items[0].name)}
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
    <div class="form-group row" style="padding-left:28px;">
        <label for="itemName" class="col-form-label">Days: </label>
        <div class="">
            <input
                type="text"
                readonly
                class="form-control-plaintext"
                id="itemName"
                value={items[0].duration}
                style="color:black;"
            />
        </div>
    </div>
</form>

<!-- list of units -->
<h3>Units</h3>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col" class="auto-width">#</th>
            <th scope="col" class="auto-width">Actions</th>
            <th scope="col">Name</th>
        </tr>
    </thead>
    <tbody>
        {#each items[0].unitList as item, i}
            <tr>
                <td scope="row">{i + 1}</td>
                <td
                    ><button
                        on:click={() => showModalRemoveUnit(item)}
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
                    <ModalComponent bind:this={modalRemoveUnit}>
                        <h1 style="text-align:center;">Are you sure?</h1>
                        <button
                            style="position:absolute;bottom: 1em;right:5%"
                            on:click={async () => {
                                await removeResource(items[0]);
                            }}>Delete</button
                        >
                    </ModalComponent></td
                >
                <td><a href="#/units/{item.name}">{item.name}</a></td>
            </tr>
        {/each}
    </tbody>
</table>

<!-- summary data visualisation (broken rn)-->
<h3>Requirement Summary</h3>
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col" class="auto-width">#</th>
            <th scope="col" class="auto-width">Name</th>
            <th scope="col" class="auto-width">Amount/day</th>
            <th scope="col" class="auto-width">Total Amount</th>
        </tr>
    </thead>
    <tbody>
        {#each summaryItems as item, i}
            <tr>
                <td scope="row">{i + 1}</td>
                <td
                    ><a href="#/requirements/{item.requirement.name}">
                        {item.requirement.name}</a
                    ></td
                >
                <td style="text-align:right;">{item.amount}</td>
                <td style="text-align:right;">{item.totalAmount}</td>
            </tr>
        {/each}
    </tbody>
</table>

<style>
    .auto-width {
        width: 1px;
    }
</style>
