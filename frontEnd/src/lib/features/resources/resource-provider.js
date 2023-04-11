
//creates requirement with a name
export const createResourceAsync = async function (resourceName, requirementId, requirementCapacity) {
    var myData = {};
    myData.ResourceName = resourceName;
    myData.RequirementId = requirementId;
    myData.RequirementCapacity = requirementCapacity;

    console.log(myData)

    let response = await fetch("http://localhost:5266/api/Resource", {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
}
export const updateRequirementAmountAsync = async function (name, id, amount) {
    const myData = { Id: id, Amount: amount };
    console.log(myData);

    const response = await fetch(`http://localhost:5266/api/Resource/${name}`, {
        headers: { 'accept': '*/*', 'content-type': 'application/json; charset=utf-8' },
        method: 'PUT',
        body: JSON.stringify(myData),
    });
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
export const getResourcesQueryAsync = async function (resourceName, returnList) {
    let response = await fetch("http://localhost:5266/api/Resource" + "/" + resourceName + "?returnList=" + returnList, { method: 'GET' })
    const data = await response.json()
    return data;
}
export const deleteResourceAsync = async function (name) {
    let response = await fetch("http://localhost:5266/api/Resource" + "/" + name, { method: 'DELETE' })
}
// export const deleteRequirementAsync = async function (requirementName) {
//     let response = await fetch(RequirementsApiUrl + "/" + requirementName, { method: 'DELETE' })
//     const data = await response.json()
//     return data;
// }

