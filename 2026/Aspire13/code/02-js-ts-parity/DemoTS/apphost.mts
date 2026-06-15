// Aspire TypeScript AppHost
// For more information, see: https://aspire.dev

import { createBuilder } from './.aspire/modules/aspire.mjs';

const builder = await createBuilder();

builder.addJavaScriptApp("myWebApp", "./web");

await builder.build().run();