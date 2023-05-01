<script>
    import UnitList from "./List.svelte";
    import {
        createUnitAsync,
        getUnitAsync,
        getUnitsAsync,
    } from "./unit-provider";
    import { getResourcesAsync } from "../resources/resource-provider";
    import ModalComponent from "../ModalComponent.svelte";
    import DetailCard from "./DetailCard.svelte";
    import Multiselect from "svelte-multiselect/src/Multiselect.svelte";

    export let params;

    const getItems = async function () {
        items = await getUnitsAsync();
    };

    let items = [];
    getItems();
    async function refreshList() {
        await getItems();
        location.href = "#/units/";
    }

    let selectItems = [];
    const resources = async function () {
        selectItems = await getResourcesAsync();
    };
    resources();

    let selected;
    let unitNameInput = "";
    let unitCapacityInput = 1;
    const addItem = async function () {
        await createUnitAsync(unitNameInput, selected.id, unitCapacityInput);
        location.href = "#/units/" + unitNameInput;
        unitNameInput = "";
        unitCapacityInput = 1;
        modalcomponent.hide();
        getItems();
    };

    let detail = "";
    async function showDetailAsync(name) {
        if (name === undefined) return;
        let response = await getUnitAsync(name);
        const formattedJson = JSON.stringify(response);
        console.log(formattedJson);

        detail = JSON.parse(formattedJson);
        // console.log(JSON.stringify(response, null, 4));
    }
    let unitName;
    async function loadDataAsync(unitName) {
        showDetailAsync(unitName);
    }
    $: {
        unitName = params?.unitName;
        loadDataAsync(unitName);
        console.log(unitName);
    }

    let modalcomponent;
</script>

<h2>Units</h2>
<div class="row">
    <div class="col-lg-3 col-md-12">
        <div class="card card-primary card-outline">
            <button
                class="btn btn-dark rounded-0"
                on:click={() => modalcomponent.show()}
                ><i
                    class="fa fa-plus"
                    style="padding: 0.5rem, 0.7rem;"
                /></button
            >
            <ModalComponent bind:this={modalcomponent}>
                    <div class="form-group">
                        <label for="unitNameInput">Name</label>
                        <input
                            id="unitNameInput"
                            type="text"
                            class="form-control"
                            bind:value={unitNameInput}
                        />
                    </div>
                    <div class="form-group">
                        <label for="unitResourceName">Resource Name</label>

                        <Multiselect
                            small
                            bind:value={selected}
                            options={selectItems}
                            multiple={false}
                            closeOnSelect={true}
                            clearOnSelect={false}
                            placeholder="Select items to add"
                            trackBy="name"
                            label="name"
                            id="unitResourceName"
                        />
                    </div>
                    <div class="form-group">
                        <label for="unitResourceAmount">Resource Amount</label>
                        <input
                            id="unitResourceAmount"
                            type="number"
                            class="form-control"
                            bind:value={unitCapacityInput}
                            min="1"
                        />
                    </div>
                    <button
                        on:click={addItem}
                        style="position:absolute;bottom: 1em;right:5%"
                        >Submit</button
                    >
            </ModalComponent>

            <div class="card-body p-0">
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <table class="table table-striped table-sm">
                                <tbody>
                                    <!-- <RequirementsList {Requirements} on:showDetail={showDetail} /> -->
                                    <UnitList
                                        {items}
                                        on:needsRefreshList={() =>
                                            refreshList()}
                                    />
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-9 col-md-12 svelte-7adzez">
        <div class="card card-primary card-outline card-tabs">
            <div class="card-body p-0" id="detailRequirement">
                {#if detail !== ""}
                    <DetailCard
                        items={detail}
                        on:needsRefresh={() => loadDataAsync(unitName)}
                        on:needsRefreshGoHome={() => refreshList()}
                    />
                {/if}
            </div>
        </div>
    </div>
</div>
