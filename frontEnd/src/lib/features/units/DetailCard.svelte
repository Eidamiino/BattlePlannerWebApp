<script>
    export let items;
    import ModalComponent from "../ModalComponent.svelte";
    import {
        deleteUnitAsync,
        updateResourceAmountAsync,
        addResourceAsync,
    } from "./unit-provider";
    import { getResourcesAsync } from "../resources/resource-provider";
    import Multiselect from "svelte-multiselect/src/Multiselect.svelte";

    let optionsResources = [];
    const fillResourceSelect = async function () {
        optionsResources = await getResourcesAsync();
    };
    fillResourceSelect();

    let selectedItem = null;
    const remove = async function () {
        if (selectedItem) {
            await deleteUnitAsync(selectedItem);
            selectedItem = null;
        }
    };
    let modalcomponent;
    const showModal = (item) => {
        selectedItem = item;
        modalcomponent.show();
    };

    let modalEditUnit;
    const showModalEditUnit = () => {
        modalEditUnit.show();
    };

    let selectedResource;
    let resourceAmountInput = 0;
    let modalAddResource;
    const showModalAddResource = () => {
        modalAddResource.show();
    };
</script>

<form on:submit|preventDefault|stopPropagation>
    <div class="form-group row">
        <label for="itemName" class="col-sm-2 col-form-label">Name: </label>
        <div class="col-sm-8">
            <input
                type="text"
                readonly
                class="form-control-plaintext"
                id="itemName"
                value={items[0].name}
                style="color:black;"
            />
        </div>

        <!-- adding items -->
        <div class="col-sm-1">
            <button
                on:click={() => {
                    showModalAddResource();
                    resourceAmountInput = 0;
                }}
                class="btn btn-success rounded-0"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Add"
                style="text-align:right;"
            >
                <i class="fa fa-plus" style="padding: 0.5rem, 0.7rem;" />
            </button>

            <ModalComponent bind:this={modalAddResource}>
                <h1 style="text-align:center;">Add resource</h1>

                <h4>Resource Name:</h4>
                <Multiselect
                    small
                    bind:value={selectedResource}
                    options={optionsResources}
                    multiple={false}
                    closeOnSelect={true}
                    clearOnSelect={false}
                    placeholder="Select item to add"
                    trackBy="name"
                    label="name"
                />

                <h4>Resource Amount</h4>
                <input
                    type="number"
                    class="form-control"
                    bind:value={resourceAmountInput}
                />

                <button
                    style="position:absolute;bottom: 1em;left:40%"
                    on:click={async () => {
                        console.log("adding:" + selectedItem);
                        await addResourceAsync(
                            items[0].name,
                            selectedResource.id,
                            resourceAmountInput
                        );
                    }}>Add</button
                >
            </ModalComponent>
        </div>

        <div class="col-sm-1">
            <button
                on:click={() => showModal(items[0].name)}
                class="btn btn-danger rounded-0"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Delete"
                style="text-align:right;"
            >
                <i class="fa fa-trash" style="padding: 0.5rem, 0.7rem;" />
            </button>
            <ModalComponent bind:this={modalcomponent}>
                <h1 style="text-align:center;">Are you sure?</h1>
                <button
                    style="position:absolute;bottom: 1em;left:40%"
                    on:click={async () => {
                        console.log("removing:" + selectedItem);
                        await remove();
                    }}>Delete</button
                >
            </ModalComponent>
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
        {#each items[0].resourceList as item, i}
            <tr>
                <th scope="row">{i + 1}</th>

                <td
                    ><a href="#/resources/{item.resource.name}"
                        >{item.resource.name}</a
                    ></td
                >
                <td>
                    <input type="number" bind:value={item.amount} />
                </td>
                <td>
                    <div class="col-sm-2">
                        <button
                            on:click={() => showModalEditUnit()}
                            class="btn btn-dark rounded-0"
                            type="button"
                            data-toggle="tooltip"
                            data-placement="top"
                            title="Edit"
                            style="text-align:right;"
                        >
                            <i
                                class="fa fa-edit"
                                style="padding: 0.5rem, 0.7rem;"
                            />
                        </button>
                        <ModalComponent bind:this={modalEditUnit}>
                            <h1 style="text-align:center;">Update amount?</h1>
                            <button
                                style="position:absolute;bottom: 1em;left:40%"
                                on:click={async () => {
                                    await updateResourceAmountAsync(
                                        items[0].name,
                                        item.resource.id,
                                        item.amount
                                    );
                                }}>Update</button
                            >
                        </ModalComponent>
                    </div>
                </td>
            </tr>
        {/each}
    </tbody>
</table>
