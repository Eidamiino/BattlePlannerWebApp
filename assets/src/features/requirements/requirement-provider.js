
//creates requirement with a name
export const createRequirementsAsync = async function (requirementName) {
    let response = await fetch("http://localhost:5266/api/Requirements", {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(requirementName)
    })
    const data = await response.json()
    return data
}
export const getRequirementsAsync = async function () {
    let data = await fetch("http://localhost:5266/api/Requirements", { method: 'GET' })
    return await data.json()
}
export const getRequirementAsync = async function (requirementName) {
    let response = await fetch("http://localhost:5266/api/Requirements/" + requirementName, { method: 'GET' })
    const data = await response.json()
    return data;
}

