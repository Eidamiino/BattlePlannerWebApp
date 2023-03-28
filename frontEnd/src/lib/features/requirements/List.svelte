<script>
    export let items;
    import {
        deleteRequirementAsync,
        getRequirementsAsync,
    } from "./requirement-provider";
    const remove = async function (name) {
        await deleteRequirementAsync(name);
        await getRequirementsAsync();
    };
</script>

{#each items as item}
    <tr>
        <td class="title"
            ><a href="#/requirements/{item.name}">{item.name}</a></td
        >
        <button
            on:click={async () => await remove(item.name)}
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
