
//creates requirement with a name
export const createResourceAsync = async function (resourceName, requirementName, requirementCapacity) {
    var myData = {};
    myData.ResourceName = resourceName;
    myData.RequirementName = requirementName;
    myData.RequirementCapacity = requirementCapacity;

    console.log(myData)

    let response = await fetch("http://localhost:5266/api/Resource", {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
    const data = await response.json()
    return data
}

export const getResourcesAsync = async function () {
    let data = await fetch("http://localhost:5266/api/Resource", { method: 'GET' })
    return await data.json()
}
export const getResourceAsync = async function (resourceName) {
    let response = await fetch("http://localhost:5266/api/Resource" + "/" + resourceName, { method: 'GET' })
    const data = await response.json()
    return data;
}
// export const deleteRequirementAsync = async function (requirementName) {
//     let response = await fetch(RequirementsApiUrl + "/" + requirementName, { method: 'DELETE' })
//     const data = await response.json()
//     return data;
// }

