<script>
    import Router, { push } from "svelte-spa-router";

    import {
        createRequirementsAsync,
        getRequirementsAsync,
        getRequirementQueryAsync,
    } from "./requirement-provider";

    import List from "./List.svelte";
    import ModalComponent from "../ModalComponent.svelte";
    import DetailCard from "./DetailCard.svelte";

    export let params;

    let items = [];
    const getItems = async function () {
        items = await getRequirementsAsync();
    };
    getItems();
    async function refreshList() {
        await getItems();
        console.trace("poseru se a refreshuju list");
        location.href = "#/requirements/";
    }

    let text = "";
    const addItem = async function () {
        console.log("Creating requirement...");
        await createRequirementsAsync(text);
        console.log("Requirement created, updating URL...", text);

        location.href = "#/requirements/" + text;

        // push(`/requirements/${text}`);

        text = "";
        modalcomponent.hide();
        getItems();
    };

    let detail = "";
    async function showDetailAsync(name) {
        if (name === undefined) return;
        let response = await getRequirementQueryAsync(name, false);
        const formattedJson = JSON.stringify(response[0]); //nevim bracho
        detail = JSON.parse(formattedJson);
    }

    let requirementName;
    async function loadDataAsync(requirementName) {
        showDetailAsync(requirementName);
    }
    $: {
        requirementName = params?.reqName;
        loadDataAsync(requirementName);
        console.log("tehotnik", requirementName);
    }

    let modalcomponent;
</script>

<h2>Requirements</h2>
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
                    <label for="requirementName">Name</label>
                    <input
                        type="text"
                        class="form-control"
                        id="requirementName"
                        bind:value={text}
                    />
                </div>
                <button
                    style="position:absolute;bottom: 1em;right:5%"
                    on:click={addItem}>Submit</button
                >
            </ModalComponent>

            <div class="card-body p-0">
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <table class="table table-striped table-sm">
                                <tbody>
                                    <!-- <RequirementsList {Requirements} on:showDetail={showDetail} /> -->
                                    <List
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
                        on:needsRefresh={() => loadDataAsync(requirementName)}
                        on:needsRefreshGoHome={() => refreshList()}
                    />
                {/if}
            </div>
        </div>
    </div>
</div>

<style>
</style>
