<script>
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
        const formattedJson = JSON.stringify(response[0]); //nevim bracho
        detail = JSON.parse(formattedJson);
    }

    $: showDetail(params?.reqName);

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
                <form>
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
                        style="position:absolute;bottom: 1em;left:40%"
                        on:click={addItem}>Submit</button
                    >
                </form>
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
                {#if detail !== ""}
                    <DetailCard items={detail} />
                {/if}
            </div>
        </div>
    </div>
</div>

<style>
</style>
