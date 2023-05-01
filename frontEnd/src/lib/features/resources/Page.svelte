<script>
    import {
        createResourceAsync,
        getResourceAsync,
        getResourcesAsync,
    } from "./resource-provider";
    import { getRequirementsAsync } from "../requirements/requirement-provider";

    import ResourceList from "./List.svelte";
    import ModalComponent from "../ModalComponent.svelte";
    import DetailCard from "./DetailCard.svelte";
    import Multiselect from "svelte-multiselect/src/Multiselect.svelte";

    export let params;

    const getItems = async function () {
        items = await getResourcesAsync();
    };
    let items = [];
    getItems();

    async function refreshList() {
        await getItems();
        location.href = "#/resources/";
    }

    let selectItems = [];
    const requirements = async function () {
        selectItems = await getRequirementsAsync();
    };
    requirements();

    let selected;
    let resourceNameInput = "";
    let resourceCapacityInput = 1;
    const addItemResource = async function () {
        await createResourceAsync(
            resourceNameInput,
            selected.id,
            resourceCapacityInput
        );
        location.href = "#/resources/" + resourceNameInput;

        resourceNameInput = "";
        resourceCapacityInput = 1;
        modalcomponent.hide();
        getItems();
    };

    let detail = "";
    async function showDetailAsync(name) {
        if (name === undefined) return;
        let response = await getResourceAsync(name);
        const formattedJson = JSON.stringify(response);
        console.log(formattedJson);

        detail = JSON.parse(formattedJson);
    }

    let resourceName;
    async function loadDataAsync(resourceName) {
        showDetailAsync(resourceName);
    }
    $: {
        resourceName = params?.resName;
        loadDataAsync(resourceName);
        console.log(resourceName);
    }

    let modalcomponent;
</script>

<h2>Resources</h2>
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
                <form>
                    <div class="form-group">
                        <label for="resourceNameInput">Name</label>
                        <input
                            id="resourceNameInput"
                            type="text"
                            class="form-control"
                            bind:value={resourceNameInput}
                        />
                    </div>
                    <div class="form-group">
                        <label for="resourceRequirementName"
                            >Requirement Name</label
                        >

                        <Multiselect
                            id="resourceRequirementName"
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
                    </div>
                    <div class="form-group">
                        <label for="requirementCapacity"
                            >Requirement Amount</label
                        >
                        <input
                            type="number"
                            class="form-control"
                            id="requirementCapacity"
                            bind:value={resourceCapacityInput}
                            min="1"
                        />
                    </div>
                    <button
                        on:click={() => addItemResource()}
                        style="position:absolute;bottom: 1em;right:5%"
                        >Submit</button
                    >
                </form>
            </ModalComponent>

            <div class="card-body p-0">
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <table class="table table-striped table-sm">
                                <tbody>
                                    <!-- <RequirementsList {Requirements} on:showDetail={showDetail} /> -->
                                    <ResourceList
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
                <!-- {@html detail} -->
                {#if detail !== ""}
                    <DetailCard
                        items={detail}
                        on:needsRefresh={() => loadDataAsync(resourceName)}
                        on:needsRefreshGoHome={() => refreshList()}
                    />
                {/if}
            </div>
        </div>
    </div>
</div>
