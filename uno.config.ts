import {defineConfig, presetAttributify, presetIcons, presetTypography, presetUno} from 'unocss'

export default defineConfig({
    theme: {
        colors: {},

    },
    presets: [
        presetIcons({scale: 1.2, warn: true}),
        presetAttributify(),
        presetUno({
            dark: "class"
        }),
        presetTypography({}),
    ],
    shortcuts: [{
         }],
    preflights: [{
        layer: 'base',
        getCSS: () => `
      :root {
      }

      html,body {
        height: 100%;
      }

      html.dark {
        --c-scroll: #333333;
        --c-scroll-hover: #555555;
        --c-shadow: #ffffff08;
      }
    `,
    }],
})