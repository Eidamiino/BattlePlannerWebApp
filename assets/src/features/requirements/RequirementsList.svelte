<script>
    export let Requirements;
    import { append } from "svelte/internal";
    import { getRequirementAsync } from "./requirement-provider";
    import { createEventDispatcher } from "svelte";
    let dispatch = createEventDispatcher();
</script>

{#each Requirements as item}
    <tr>
        <td class="title"
            ><a
                href="javascript:;"
                on:click={async () => {
                    dispatch(
                        "showDetail",
                        await getRequirementAsync(item.name)
                    );
                    // console.log(await getRequirementAsync(item.name))}
                }}>{item.name}</a
            ></td
        >
    </tr>
{/each}
