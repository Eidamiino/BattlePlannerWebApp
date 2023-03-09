const renderResourcesAsync = async function () {
    var data = await fetch(ResourcesApiUrl, { method: 'GET' })
    var dataJson = await data.json()
    await renderResourcesListAsync(dataJson, "resources", "resourceList")
}
//puts all requirements to an unordered list
const renderResourcesListAsync = async function (resources, templateId, listId) {
    var template = ($(`#${templateId}`)).html();
    Mustache.parse(template);

    $(`#${listId}`).empty()
    resources.forEach(requirement => {
        let renderRequirements = Mustache.render(template, requirement)
        $(`#${listId}`).append(renderRequirements)
    })

}
