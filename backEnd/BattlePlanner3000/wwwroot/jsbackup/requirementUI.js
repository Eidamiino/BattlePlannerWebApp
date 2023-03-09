const renderRequirementsAsync = async function () {
    var data = await fetch(RequirementsApiUrl, { method: 'GET' })
    var dataJson = await data.json()
    await renderRequirementsListAsync(dataJson, "requirements", "requirementList")
    await fillSelectRequirementsAsync(dataJson, "choices")
}
//fills a select with every requirement
const fillSelectRequirementsAsync = async function (reqs, selectId) {
    $(`#${selectId}`).empty()
    reqs.forEach(requirement => {
        let option = document.createElement("option")
        option.value = requirement.name;
        option.text = requirement.name;
        $(`#${selectId}`).append(option)
    })
}
//puts all requirements to an unordered list
const renderRequirementsListAsync = async function (reqs, templateId, listId) {
    var template = ($(`#${templateId}`)).html();
    Mustache.parse(template);

    $(`#${listId}`).empty()
    reqs.forEach(requirement => {
        let renderRequirements = Mustache.render(template, requirement)
        $(`#${listId}`).append(renderRequirements)
    })

}