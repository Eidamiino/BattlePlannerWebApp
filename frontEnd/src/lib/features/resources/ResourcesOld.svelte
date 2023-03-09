<script>
    import ResourceList from "./ResourceList.svelte";
    import { getRequirementsAsync } from "../requirements/requirement-provider";
    import {
        createResourceAsync,
        getResourcesAsync,
    } from "./resource-provider";

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
        resourceNameInput = "";
        resourceCapacityInput = 0;
        req();
    };
</script>

<h1>Resources</h1>
<table>
    <tr>
        <th>Resource Name</th>
    </tr>
    <tr>
        <th>Requirement Name</th>
    </tr>
    <tr>
        <th>Requirement Capacity</th>
    </tr>
    <ResourceList {Resources} />
</table>

<div class="mb-3">
    <div class="form-label">Name:</div>
    <input
        type="text"
        class="form-control"
        id="resourceName"
        bind:value={resourceNameInput}
    />
</div>

<div class="mb-3">
    <div class="form-label">Requirement Name:</div>

    <select bind:value={selected}>
        {#each Requirements as item}
            <option value={item.name}>{item.name}</option>
        {/each}
    </select>
    <!-- <ResourceRequirementFill {Requirements} /> -->
</div>

<div class="mb-3">
    <div class="form-label">Requirement Amount:</div>
    <input
        type="number"
        class="form-control"
        id="requirementCapacity"
        bind:value={resourceCapacityInput}
    />
</div>

<div class="mb-3">
    <button on:click={addResource} class="btn btn-primary">Submit</button>
</div>
