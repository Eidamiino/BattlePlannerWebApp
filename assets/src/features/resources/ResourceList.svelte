<script>
    export let Resources;
</script>

{#each Resources as item}
    <tr
        ><td>{JSON.stringify(item.name)}</td><td
            >{JSON.stringify(item.requirementList[0].requirement.name)}</td
        ><td>{JSON.stringify(item.requirementList[0].amount)}</td></tr
    >
{/each}
