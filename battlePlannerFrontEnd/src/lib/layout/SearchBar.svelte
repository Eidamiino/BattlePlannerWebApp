<script>
    import { createEventDispatcher } from "svelte";
    const dispatch = createEventDispatcher();
    let query = "";
    function handleInput(event) {
        query = event.target.value;
        console.log(query);
        dispatch("search", query);
    }
</script>

<input
    class="form-control form-control-dark w-100 search-input"
    type="text"
    placeholder="Search"
    aria-label="Search"
    bind:value={query}
    on:input={handleInput}
/>
