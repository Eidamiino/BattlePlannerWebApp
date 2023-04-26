<script>
    let shown = false;
    export async function show() {
        shown = true;
    }
    export async function hide() {
        shown = false;
    }
</script>

{#if shown}
    <div class="modal">
        <div class="modal-content">
            <span class="close" on:click={async () => await hide()}
                >&times;</span
            >

            <slot />
            <button
                style="position:absolute;bottom: 1em;right:20%"
                on:click={async () => await hide()}
            >
                Cancel
            </button>
        </div>
    </div>
{/if}

<style>
    .modal {
        background-color: rgba(0, 0, 0, 0.6);
        position: fixed;
        width: 100%;
        height: 100%;
        top: 0;
        left: 0;
        display: flex;
    }
    .close {
        text-align: right;
    }

    .modal-content {
        /* background-color: white;
        padding: 20px; */
        background-color: white;
        max-width: 30vw;
        padding: 1rem;
        margin: 15% auto;

        /* height: 25%; */

        display: flex;

        flex-wrap: wrap;
        height: auto;
        /* max-height: 160px; */
    }
</style>
