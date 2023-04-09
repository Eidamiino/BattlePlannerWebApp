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
    export let items;
    import {
        // deleteRequirementAsync,
        getUnitsAsync,
        deleteUnitAsync,
    } from "./unit-provider";
    const remove = async function (name) {
        await deleteUnitAsync(name);
        await getUnitsAsync();
    };
</script>

{#each items as item}
    <tr>
        <td class="title"><a href="#/units/{item.name}">{item.name}</a></td>
        <button
            on:click={() => remove(item.name)}
            class="btn btn-danger rounded-0"
            type="button"
            data-toggle="tooltip"
            data-placement="top"
            title="Delete"
        >
            <i class="fa fa-trash" style="padding: 0.5rem, 0.7rem;" />
        </button>
    </tr>
{/each}
