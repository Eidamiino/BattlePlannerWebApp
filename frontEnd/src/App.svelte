<script>
  import { onMount } from "svelte";

  import NavBar from "./lib/layout/NavBar.svelte";
  import SideBar from "./lib/layout/SideBar.svelte";

  import Router, { location, push } from "svelte-spa-router";
  import routes from "./routes";
  import { getRequirementQueryAsync } from "./lib/features/requirements/requirement-provider";
  import { getResourcesQueryAsync } from "./lib/features/resources/resource-provider";
  import { getUnitsQueryAsync } from "./lib/features/units/unit-provider";
  import { getPlansQueryAsync } from "./lib/features/battlePlan/battlePlan-provider";

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
      case "planBattle": {
        return await getPlansQueryAsync(query, true);
      }
      default: {
        console.log("spatna url hroch");
      }
    }
  }
  async function handleRedirectSearch(event) {
    console.log("mciinyobtyut", event.detail.name);
    push("/" + currentPage + "/" + event.detail.name);
  }

  $: {
    currentPage = $location.split("/")[1];
    const navLinks = document.querySelectorAll(".sidebar .nav-link");
    navLinks.forEach((navLink) => {
      if (navLink.getAttribute("href") === `/#/${currentPage}`) {
        navLink.classList.add("active");
      } else {
        navLink.classList.remove("active");
      }
    });
  }
  onMount(() => {
    performSearch("initial query");
  });
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
