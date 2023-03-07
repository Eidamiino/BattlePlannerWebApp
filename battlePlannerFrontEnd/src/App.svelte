<script>
  import NavBar from "./lib/layout/NavBar.svelte";
  import SideBar from "./lib/layout/SideBar.svelte";

  import Router, { location, link } from "svelte-spa-router";
  import routes from "./routes";
</script>

<NavBar />
<div class="container-fluid">
  <div class="row">
    <SideBar />
    <main role="main" class="col-md-9 ml-sm-auto col-lg-10 pt-3 px-4">
      <Router {routes} />
    </main>
  </div>
</div>
