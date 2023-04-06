<script>
    export let items;
</script>

<form>
    <div class="form-group row">
        <label for="itemName" class="col-sm-2 col-form-label">Name: </label>
        <div class="col-sm-10">
            <input
                type="text"
                readonly
                class="form-control-plaintext"
                id="itemName"
                value={items[0].name}
                style="color:black;"
            />
        </div>
    </div>
</form>

<!-- summary data visualisation (broken rn)-->
<!-- <table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
            <th scope="col">Amount</th>
        </tr>
    </thead>
    <tbody>
        {#each Object.keys(items[0].summary) as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td>{item}</td>
                <td>{items.summary[item]}</td>
            </tr>
        {/each}
    </tbody>
</table> -->

<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
        </tr>
    </thead>
    <tbody>
        {#each items[0].unitList as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td>{item.name}</td>
            </tr>
        {/each}
    </tbody>
</table>
