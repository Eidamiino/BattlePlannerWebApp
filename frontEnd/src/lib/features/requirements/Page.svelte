<script>
    import List from "./List.svelte";
    import {
        createRequirementsAsync,
        getRequirementsAsync,
        getRequirementQueryAsync,
    } from "./requirement-provider";

    import ModalComponent from "../ModalComponent.svelte";
    export let params;

    let items = [];
    const getItems = async function () {
        items = await getRequirementsAsync();
    };
    getItems();

    let text = "";
    const addItem = async function () {
        await createRequirementsAsync(text);
        location.href = "#/requirements/" + text;
        text = "";
        modalcomponent.hide();
        getItems();
    };

    let detail = "";
    async function showDetail(name) {
        if (name === undefined) return;
        let response = await getRequirementQueryAsync(name, false);
        detail = JSON.stringify(response)
            .replace(/[{}]/g, "")
            .replace(/\[|\]/g, "")
            .replace(/"/g, "");
    }

    $: showDetail(params?.reqName);

    let modalcomponent;
</script>

<h2>Requirements</h2>
<div class="row">
    <div class="col-lg-3 col-md-12">
        <div class="card card-primary card-outline">
            <button on:click={() => modalcomponent.show()}>Add</button>
            <ModalComponent bind:this={modalcomponent}>
                <h2>Name:</h2>
                <input
                    type="text"
                    class="form-control"
                    id="requirementName"
                    bind:value={text}
                />
                <button
                    style="position:absolute;bottom: 1em;left:40%"
                    on:click={addItem}>Submit</button
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
                                    <List {items} />
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
                    {@html detail}
                </h2>
            </div>
        </div>
    </div>
</div>

<style>
</style>
