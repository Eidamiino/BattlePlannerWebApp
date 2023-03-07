<!-- <script>
    export let Resources;
</script>

{#each Resources as item}
    <tr
        ><td>{JSON.stringify(item.name)}</td><td
            >{JSON.stringify(item.requirementList[0].requirement.name)}</td
        ><td>{JSON.stringify(item.requirementList[0].amount)}</td></tr
    >
{/each} -->
<script>
    export let Resources;
    import {
        // deleteRequirementAsync,
        getResourcesAsync,
    } from "./resource-provider";
</script>

{#each Resources as item}
    <tr>
        <td class="title"><a href="#/resources/{item.name}">{item.name}</a></td>
        <button
            class="btn btn-danger btn-sm rounded-0"
            type="button"
            data-toggle="tooltip"
            data-placement="top"
            title="Delete"><i class="fa fa-trash" /></button
        >
    </tr>
{/each}
