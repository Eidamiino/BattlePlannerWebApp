import { RequirementsApiUrl } from "../../../constants"
console.log("miciny", RequirementsApiUrl)
//creates requirement with a name
export const createRequirementsAsync = async function (requirementName) {
    let response = await fetch(RequirementsApiUrl, {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(requirementName)
    })
}
export const getRequirementsAsync = async function () {
    let data = await fetch(RequirementsApiUrl, { method: 'GET' })
    return await data.json()
}
export const getRequirementAsync = async function (requirementName) {
    let response = await fetch(`${RequirementsApiUrl}/${requirementName}`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getRequirementQueryAsync = async function (requirementName, returnList) {
    let response = await fetch(`${RequirementsApiUrl}/${requirementName}?returnList=${returnList}`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const getResourcesContainingAsync = async function (requirementName) {
    let response = await fetch(`${RequirementsApiUrl}/${requirementName}/contained`, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const deleteRequirementAsync = async function (requirementName) {
    let response = await fetch(`${RequirementsApiUrl}/${requirementName}`, { method: 'DELETE' })
}

