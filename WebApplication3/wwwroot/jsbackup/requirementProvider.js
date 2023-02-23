
//creates requirement with a name
const createRequirementAsync = async function (requirementName) {
    let response = await fetch(RequirementsApiUrl, {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(requirementName)
    })
    const data = await response.json()
    return data
}
