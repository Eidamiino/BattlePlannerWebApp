
//creates requirement with a name
const createResourceAsync = async function (resourceName, requirementName, requirementCapacity) {
    var myData = {};
    myData.ResourceName = resourceName;
    myData.RequirementName = requirementName;
    myData.RequirementCapacity = requirementCapacity;

    console.log(myData)

    let response = await fetch(ResourcesApiUrl, {
        headers: { 'accept': "*/*", 'content-type': "application/json; charset=utf-8" },
        method: 'POST',
        body: JSON.stringify(myData)
    })
    const data = await response.json()
    return data
}

