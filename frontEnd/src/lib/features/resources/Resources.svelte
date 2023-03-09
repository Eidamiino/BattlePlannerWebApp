<script>
    import ResourceList from "./ResourceList.svelte";
    import {
        createResourceAsync,
        getResourceAsync,
        getResourcesAsync,
    } from "./resource-provider";
    import { getRequirementsAsync } from "../requirements/requirement-provider";
    import ModalComponent from "../ModalComponent.svelte";
    export let params;

    const req = async function () {
        Resources = await getResourcesAsync();
    };

    let Resources = [];
    req();

    const requirements = async function () {
        Requirements = await getRequirementsAsync();
    };
    let Requirements = [];
    requirements();

    let selected;
    let resourceNameInput = "";
    let resourceCapacityInput = 0;
    const addResource = async function () {
        await createResourceAsync(
            resourceNameInput,
            selected,
            resourceCapacityInput
        );
        location.href = "#/resources/" + resourceNameInput;
        resourceNameInput = "";
        resourceCapacityInput = 0;
        modalcomponent.hide();
        req();
    };

    let detail = "";
    async function showDetail(name) {
        if (name === undefined) return;
        let response = await getResourceAsync(name);
        // detail = JSON.stringify(response, null, 4);
        const formattedJson = JSON.stringify(response, null, 4);
        console.log(formattedJson);
        const indentedJson = formattedJson
            .replace(/"/g, "")
            .replace(/({|}|\[|\])/g, "")
            .trim()
            .replace(/^(\s+)/gm, "$1&emsp;")
            .replace(/ /g, "&nbsp;")
            .replace(/\n/g, "<br>");
        detail = indentedJson;
        // console.log(JSON.stringify(response, null, 4));
    }

    $: showDetail(params?.resName);

    let modalcomponent;
</script>

<h2>Resources</h2>
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
                    bind:value={resourceNameInput}
                />
                <h4>Requirement Name:</h4>
                <select
                    bind:value={selected}
                    style="background-color:light-gray;"
                >
                    {#each Requirements as item}
                        <option value={item.name}>{item.name}</option>
                    {/each}
                </select>
                <h4>Requirement Amount</h4>
                <input
                    type="number"
                    class="form-control"
                    id="requirementCapacity"
                    bind:value={resourceCapacityInput}
                />

                <button
                    on:click={addResource}
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
                                    <ResourceList {Resources} />
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
