<script>
    import PlanList from "./List.svelte";
    import {
        createPlanAsync,
        getPlanAsync,
        getPlansAsync,
    } from "./battlePlan-provider";
    import { getUnitsAsync } from "../units/unit-provider";
    import ModalComponent from "../ModalComponent.svelte";
    import DetailCard from "./DetailCard.svelte";
    import Multiselect from "svelte-multiselect/src/Multiselect.svelte";

    export let params;

    const getItems = async function () {
        items = await getPlansAsync();
    };

    let items = [];
    getItems();

    let selectItems = [];
    const units = async function () {
        selectItems = await getUnitsAsync();
    };
    units();

    let selected;
    let planNameInput = "";
    let amountOfDays = 0;
    const addItem = async function () {
        await createPlanAsync(planNameInput, selected.id, amountOfDays);
        location.href = "#/planBattle/" + planNameInput;
        planNameInput = "";
        amountOfDays = 0;
        modalcomponent.hide();
        getItems();
    };

    let detail = "";
    async function showDetail(name) {
        if (name === undefined) return;
        let response = await getPlanAsync(name);
        const formattedJson = JSON.stringify(response);
        console.log(formattedJson);

        detail = JSON.parse(formattedJson);
        // console.log(JSON.stringify(response, null, 4));
    }

    $: showDetail(params?.planName);

    let modalcomponent;
</script>

<h2>Battle Plans</h2>
<div class="row">
    <div class="col-lg-3 col-md-12">
        <div class="card card-primary card-outline">
            <button on:click={() => modalcomponent.show()}>Add</button>
            <ModalComponent bind:this={modalcomponent}>
                <h4>Name:</h4>
                <input
                    type="text"
                    class="form-control"
                    id="requirementName"
                    bind:value={planNameInput}
                />
                <h4>Unit Name:</h4>
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
                />
                <h4>Amount of Days</h4>
                <input
                    type="number"
                    class="form-control"
                    id="requirementCapacity"
                    bind:value={amountOfDays}
                />

                <button
                    on:click={addItem}
                    style="position:absolute;bottom: 1em;left:40%"
                    >Submit</button
                >
            </ModalComponent>

            <div class="card-body p-0">
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <table class="table table-striped table-sm">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <!-- <RequirementsList {Requirements} on:showDetail={showDetail} /> -->
                                    <PlanList {items} />
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
                <h2>
                    {#if detail !== ""}
                        <DetailCard items={detail} />
                    {/if}
                </h2>
            </div>
        </div>
    </div>
</div>
