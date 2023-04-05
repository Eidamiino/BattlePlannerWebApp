<script>
    export let items;
    // function formatPropertyName(propertyName) {
    //     let words = propertyName.split(/(?=[A-Z])/);
    //     return words
    //         .map((word) => word.charAt(0).toUpperCase() + word.slice(1))
    //         .join(" ");
    // }

    // let formattedPropertyNames = Object.keys(resource).map(formatPropertyName);
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
<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
            <th scope="col">Amount</th>
        </tr>
    </thead>
    <tbody>
        {#each items[0].requirementList as item, i}
            <tr>
                <th scope="row">{i + 1}</th>
                <td>{item.requirement.name}</td>
                <td>{item.amount}</td>
            </tr>
        {/each}
    </tbody>
</table>
