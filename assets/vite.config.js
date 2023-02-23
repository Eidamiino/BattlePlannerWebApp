import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'


// https://vitejs.dev/config/
export default defineConfig({
  plugins: [svelte()],
  build: {
    emptyOutDir: true,
    minify: "esbuild",
    sourcemap: true,
    lib: {
      entry: `src/main.js`,
      name: "app",
      fileName: `main`,
    },
    outDir: "../WebApplication3/wwwroot/build",
  },
  resolve: {
    dedupe: ["svelte"],
  },
  plugins: [
    svelte({
      dev: true,
    }),
  ],
}
)
