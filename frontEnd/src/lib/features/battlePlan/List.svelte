<script>
    export let items;
    import { deletePlanAsync, getPlansAsync } from "./battlePlan-provider";
    const remove = async function (name) {
        await deletePlanAsync(name);
        await getPlansAsync();
    };
</script>

{#each items as item}
    <tr>
        <td class="title"><a href="#/planBattle/{item.name}">{item.name}</a></td
        >
        <button
            on:click={() => remove(item.name)}
            class="btn btn-danger btn-sm rounded-0"
            type="button"
            data-toggle="tooltip"
            data-placement="top"
            title="Delete"><i class="fa fa-trash" /></button
        >
    </tr>
{/each}

<!-- on:click={async () => {
                    dispatch(
                        "showDetail",
                        await getRequirementAsync(item.name)
                    );
                }}
             -->

<!-- on:click={async () => {
                await deleteRequirementAsync(item.name);
            }} -->
