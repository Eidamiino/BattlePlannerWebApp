import Dashboard from './lib/features/home/Dashboard.svelte';
// import Page1 from './lib/features/pages/Page1.svelte';
// import Page2 from './lib/features/pages/Page2.svelte';
// import Page3 from './lib/features/pages/Page3.svelte';
import Requirements from './lib/features/requirements/Page.svelte';
import Resources from './lib/features/resources/Page.svelte';

const Pages = [
    // {
    //     name: "Page1",
    //     url: "/page1",
    // },
    // {
    //     name: "Page2",
    //     url: "/page2",
    // },
    // {
    //     name: "Page3",
    //     url: "/page3",
    // }
    {
        name: "Requirements",
        url: "/requirements",
    },
    {
        name: "RequirementDetail",
        url: "/requirements/:reqName",
    },
    {
        name: "Resources",
        url: "/resources",
    },
    {
        name: "ResourceDetail",
        url: "/resources/:resName",
    },
];

const PageUrls = Pages.reduce((acc, x) => {
    acc[x.name] = x.url;
    return acc;
}, {});

export default {
    [PageUrls.Requirements]: Requirements,
    [PageUrls.RequirementDetail]: Requirements,
    [PageUrls.Resources]: Resources,
    [PageUrls.ResourceDetail]: Resources,
    '*': Dashboard,
};