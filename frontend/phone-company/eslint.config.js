import js from '@eslint/js';
import typescriptEslint from '@typescript-eslint/eslint-plugin';
import angularEslint from '@angular-eslint/eslint-plugin';
import angularEslintTemplate from '@angular-eslint/eslint-plugin-template';
import angularEslintTemplateParser from '@angular-eslint/template-parser';
import changeDetectionStrategy from 'eslint-plugin-change-detection-strategy';
import simpleImportSort from 'eslint-plugin-simple-import-sort';
import unusedImports from 'eslint-plugin-unused-imports';
import importPlugin from 'eslint-plugin-import';
import typescriptParser from '@typescript-eslint/parser';
import prettierPlugin from 'eslint-plugin-prettier';
import prettierConfig from 'eslint-config-prettier';

export default [
  {
    ignores: ['projects/**/*', 'node_modules/*', 'dist/*', 'coverage/*', '.storybook/**/*'],
  },
  {
    files: ['src/**/*.ts'],
    languageOptions: {
      ecmaVersion: 2021,
      sourceType: 'module',
      globals: {
        ...js.configs.recommended.globals,
        document: 'readonly',
        navigator: 'readonly',
        window: 'readonly',
      },
      parser: typescriptParser,
      parserOptions: {
        project: ['tsconfig.json', 'tsconfig.app.json', 'tsconfig.spec.json'],
        sourceType: 'module',
        ecmaVersion: 2021,
        experimentalDecorators: true,
      },
    },
    plugins: {
      '@typescript-eslint': typescriptEslint,
      '@angular-eslint': angularEslint,
      'change-detection-strategy': changeDetectionStrategy,
      'simple-import-sort': simpleImportSort,
      'unused-imports': unusedImports,
      import: importPlugin,
      prettier: prettierPlugin,
    },
    rules: {
      ...typescriptEslint.configs.recommended.rules,
      ...angularEslint.configs.recommended.rules,
      ...prettierConfig.rules,
      'prettier/prettier': 'error',
      '@angular-eslint/directive-selector': [
        'error',
        {
          type: 'attribute',
          prefix: 'app',
          style: 'camelCase',
        },
      ],
      '@angular-eslint/component-selector': [
        'error',
        {
          type: 'element',
          prefix: 'app',
          style: 'kebab-case',
        },
      ],
      '@typescript-eslint/explicit-function-return-type': [
        'error',
        {
          allowExpressions: true,
          allowTypedFunctionExpressions: true,
        },
      ],
      '@typescript-eslint/no-explicit-any': 'error',
      '@typescript-eslint/no-unused-vars': [
        'error',
        {
          vars: 'all',
          args: 'after-used',
          varsIgnorePattern: '^_',
          argsIgnorePattern: '^_',
        },
      ],
      '@typescript-eslint/member-ordering': 'error',
      '@typescript-eslint/naming-convention': [
        'error',
        {
          selector: 'interface',
          format: ['PascalCase'],
          prefix: ['I'],
        },
      ],
      'change-detection-strategy/on-push': 'error',
      'simple-import-sort/imports': 'error',
      'simple-import-sort/exports': 'error',
      'unused-imports/no-unused-imports': 'error',
      'unused-imports/no-unused-vars': [
        'warn',
        {
          vars: 'all',
          args: 'after-used',
          varsIgnorePattern: '^_',
          argsIgnorePattern: '^_',
        },
      ],
      'import/no-duplicates': 'error',
      indent: 'off',
      quotes: 'off',
      semi: 'off',
      'linebreak-style': 'off',
      '@typescript-eslint/indent': 'off',
      '@typescript-eslint/quotes': 'off',
      '@typescript-eslint/semi': 'off',
    },
  },
  {
    files: ['src/**/*.html'],
    languageOptions: {
      parser: angularEslintTemplateParser,
    },
    plugins: {
      '@angular-eslint/template': angularEslintTemplate,
    },
    rules: {
      ...angularEslintTemplate.configs.recommended.rules,
      ...angularEslintTemplate.configs.accessibility.rules,
    },
  },
];
