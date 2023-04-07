<script>
    export let items;
    console.log(items);
</script>

<form on:submit|preventDefault|stopPropagation>
    <div class="form-group row">
        <label for="itemName" class="col-sm-2 col-form-label">Name: </label>
        <div class="col-sm-10">
            <input
                type="text"
                readonly
                class="form-control-plaintext"
                id="itemName"
                value={items.name}
                style="color:black;"
            />
        </div>
    </div>
</form>
