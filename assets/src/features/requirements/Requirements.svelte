<script>
  import RequirementsList from "./RequirementsList.svelte";
  import {
    createRequirementsAsync,
    getRequirementsAsync,
    getRequirementAsync,
    deleteRequirementAsync,
  } from "./requirement-provider";
  export let params;

  const req = async function () {
    Requirements = await getRequirementsAsync();
  };

  let Requirements = [];
  req();

  let text = "";
  const addRequirement = async function () {
    await createRequirementsAsync(text);
    text = "";
    req();
  };

  let detail = "";
  async function showDetail(name) {
    detail = await getRequirementAsync(name);
  }

  $: showDetail(params?.reqName);
</script>

<div class="row">
  <div class="col-lg-3 col-md-12">
    <div class="card card-primary card-outline">
      <div class="card-header">
        <h5 class="card-title">Requirements:</h5>
      </div>
      <div class="form-label">Name:</div>
      <input
        type="text"
        class="form-control"
        id="requirementName"
        bind:value={text}
      />
      <button on:click={addRequirement} class="btn btn-primary">Submit</button>
      <div class="card-body p-0">
        <div class="row">
          <div class="col-12">
            <table class="table table-sm data-table localesList">
              <thead>
                <tr>
                  <th>Name</th>
                </tr>
              </thead>
              <tbody>
                <!-- <RequirementsList {Requirements} on:showDetail={showDetail} /> -->
                <RequirementsList {Requirements} />
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="col-lg-9 col-md-12 svelte-7adzez">
    <div class="card card-primary card-outline card-tabs">
      <div class="card-body p-0" id="detailRequirement">
        <h2>
          {JSON.stringify(detail).replaceAll('"', "")}
        </h2>
      </div>
    </div>
  </div>
</div>
