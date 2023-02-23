import Requirements from './features/requirements/Requirements.svelte'
import Resources from './features/resources/Resources.svelte'
import NotFound from './features/not-found/NotFound.svelte'

export default {
    '/requirements': Requirements,
    '/resources': Resources,
    '*': NotFound
}