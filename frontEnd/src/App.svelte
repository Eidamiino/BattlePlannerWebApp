<script>
  import NavBar from "./lib/layout/NavBar.svelte";
  import SideBar from "./lib/layout/SideBar.svelte";

  import Router, { location, push } from "svelte-spa-router";
  import routes from "./routes";
  import { getRequirementQueryAsync } from "./lib/features/requirements/requirement-provider";
  import { getResourcesQueryAsync } from "./lib/features/resources/resource-provider";
  import { getUnitsQueryAsync } from "./lib/features/units/unit-provider";

  let searchResults;
  async function handleSearch(event) {
    const query = event.detail;
    searchResults = await performSearch(query);
  }

  $: currentPage = $location.split("/")[1];

  async function performSearch(query) {
    switch (currentPage) {
      case "requirements": {
        return await getRequirementQueryAsync(query, true);
      }
      case "resources": {
        return await getResourcesQueryAsync(query, true);
      }
      case "units": {
        return await getUnitsQueryAsync(query, true);
      }
      default: {
        console.log("hroch");
      }
    }
  }
  async function handleRedirectSearch(event) {
    // console.log(event.detail.name);
    push("/" + currentPage + "/" + event.detail.name);
  }
</script>

<NavBar
  on:search={handleSearch}
  bind:searchResults
  on:redirectSearch={handleRedirectSearch}
/>
<div class="container-fluid">
  <div class="row">
    <SideBar />
    <main role="main" class="col-md-9 ml-sm-auto col-lg-10 pt-3 px-4">
      <Router {routes} />
    </main>
  </div>
</div>
