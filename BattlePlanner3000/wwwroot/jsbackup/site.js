// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// fetch('/data', { method: 'POST' })
// 	.then(response => response.json())
// 	.then(data => console.log(data))


// function onBtnCreateRequirement() {
// 	$("#requirementList").empty()
// 	var text = document.getElementById("requirementName");
// 	renderRequirements(createRequirement(text.value))
// 	text.value = ""
// 	//createRequirement(text.value, renderRequirements())
// }

// function renderRequirements() {
// 	var select = document.getElementById("choices");

// 	let requirements = $('#requirements').html();
// 	Mustache.parse(requirements);

// 	fetch('/api/Requirements', { method: 'GET' })
// 		.then(response => response.json())
// 		.then(data => data.forEach(requirement => {
// 			let option = document.createElement("option")
// 			option.value = requirement.name;
// 			option.text = requirement.name;
// 			select.append(option)

// 			let renderRequirements = Mustache.render(requirements, requirement)
// 			console.log(renderRequirements)
// 			$("#requirementList").append(renderRequirements)
// 		}))

// }

//creates and displays a requirement with a name from input and clears the input text
async function onBtnCreateRequirementAsync() {
	var text = document.getElementById("requirementName");
	await createRequirementAsync(text.value)
	await renderRequirementsAsync()
	text.value = ""
}
async function onBtnCreateResourceAsync() {
	var inputName = document.getElementById("ResourceName");
	var select = document.getElementById("choices");
	var inputCapacity = document.getElementById("RequirementCapacity");
	var option = select.options[select.selectedIndex].text
	console.log(option)
	// var option = select.options[e.selectedIndex].text;

	await createResourceAsync(inputName.value, option, inputCapacity.value)
	await renderResourcesAsync()

	inputName.value = ""
	inputCapacity.value = ""

}


function renderResources() {
	let resources = $('#resources').html();
	Mustache.parse(resources);

	fetch(ResourcesApiUrl, { method: 'GET' })
		.then(response => response.json())
		.then(data => data.forEach(resource => {

			let renderResources = Mustache.render(resources, resource)
			console.log(renderResources)
			$("#resourceList").append(renderResources)
		}))

}
