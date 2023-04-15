<script>
    export let items;
    import ModalComponent from "../ModalComponent.svelte";
    import {
        addUnitAsync,
        deletePlanAsync,
        getSummaryAsync,
    } from "./battlePlan-provider";
    import Multiselect from "svelte-multiselect/src/Multiselect.svelte";
    import { getUnitsAsync } from "../units/unit-provider";
    import { onMount } from "svelte";

    //fill select
    let optionsUnits = [];
    const fillUnitSelect = async function () {
        optionsUnits = await getUnitsAsync();
    };
    fillUnitSelect();

    //get summary
    let summaryItems = [];
    const fillSummary = async function () {
        summaryItems = await getSummaryAsync(items[0].name);
    };
    onMount(fillSummary);

    $: {
        fillSummary();
    }

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
</script>

<form on:submit|stopPropagation>
    <div class="form-group row">
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
        <!-- adding units -->
        <div class="col-sm-1">
            <button
                on:click={() => {
                    showModalAddPlan();
                }}
                class="btn btn-success rounded-0"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Add"
                style="text-align:right;"
            >
                <i class="fa fa-plus" style="padding: 0.5rem, 0.7rem;" />
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
                    style="position:absolute;bottom: 1em;left:40%"
                    on:click={async () => {
                        await addUnitToList(items[0]);
                    }}>Add</button
                >
            </ModalComponent>
        </div>
        <!-- removing plans -->
        <div class="col-sm-1">
            <button
                on:click={() => showModal(items[0].name)}
                class="btn btn-danger rounded-0"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Delete"
                style="text-align:right;"
            >
                <i class="fa fa-trash" style="padding: 0.5rem, 0.7rem;" />
            </button>
            <ModalComponent bind:this={modalcomponent}>
                <h1 style="text-align:center;">Are you sure?</h1>
                <button
                    style="position:absolute;bottom: 1em;left:40%"
                    on:click={async () => {
                        console.log("removing:" + selectedItem);
                        await remove();
                    }}>Delete</button
                >
            </ModalComponent>
        </div>
    </div>
</form>

<!-- list of units -->
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
        </tr>
    </thead>
    <tbody>
        {#each items[0].unitList as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td><a href="#/units/{item.name}">{item.name}</a></td>
            </tr>
        {/each}
    </tbody>
</table>

<!-- summary data visualisation (broken rn)-->
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
            <th scope="col">Amount</th>
        </tr>
    </thead>
    <tbody>
        {#each summaryItems as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td>{item.requirement.name}</td>
                <td>{item.amount}</td>
            </tr>
        {/each}
    </tbody>
</table>
