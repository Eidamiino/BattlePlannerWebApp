<script>
    import SearchBar from "./SearchBar.svelte";
    import { getRequirementQueryAsync } from "../features/requirements/requirement-provider";
    import { onMount, onDestroy } from "svelte";
    import { createEventDispatcher } from "svelte";

    export let searchResults = [];
    async function handleClick(event) {
        if (!event.target.closest(".search-results")) {
            searchResults = null;
        }
    }
    onMount(() => {
        document.addEventListener("click", handleClick);
    });

    onDestroy(() => {
        document.removeEventListener("click", handleClick);
    });
    const dispatch = createEventDispatcher();
</script>

<nav class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0">
    <a
        class="navbar-brand col-sm-3 col-md-2 mr-0"
        href="https://getbootstrap.com/docs/4.0/examples/dashboard/#"
        >Battle Planner 3000</a
    >
    <div class="position-relative w-100">
        <SearchBar on:search />
        {#if searchResults?.length > 0}
            <div class="position-absolute bg-white w-100 search-results">
                <ul class="list-unstyled">
                    {#each searchResults as result}
                        <a on:click={() => dispatch("redirectSearch", result)}
                            ><li>{result.name}</li></a
                        >
                    {/each}
                </ul>
            </div>
        {/if}
    </div>
    <ul class="navbar-nav px-3">
        <li class="nav-item text-nowrap">
            <a
                class="nav-link"
                href="https://getbootstrap.com/docs/4.0/examples/dashboard/#"
                >Sign out</a
            >
        </li>
    </ul>
</nav>
