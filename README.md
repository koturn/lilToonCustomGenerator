lilToonCustomGenerator
======================

lilToon custom shader generator.

## Description

This is a Unity Editor extension designed to simplify the first steps of creating custom shaders in lilToon.
Specifically, it is a tool that expands a set of template files while considering the input shader name and property names.
Its primary purpose is to free you from the hassle of ensuring consistency between files.

- Shader name, Inspector class name
    - `Shader/lilCustomShaderDatas.lilblock`
    - `Editor/CustomInspector.cs`
- Shader property name
    - `Shader/lilCustomShaderDatas.lilblock`
    - `Shader/custom.hlsl`
    - `Shader/lilCustomShaderProperties.lilblock`
- Tag name of language file
    - `Shader/lilCustomShaderProperties.lilblock`
    - `Editor/lang_custom.tsv`
- Language file GUID
    - `Editor/CustomInspector.cs`
    - `Editor/lang_custom.tsv`

Additionally, it supports generating optional files such as a language file (`lang_custom.csv`) and `AssemblyInfo.cs`, allowing you to choose whether to generate them.

It is also possible to create custom shader files for lilToon that utilize geometry shaders.

> [!NOTE]
> This tool does not aim to support all lilToon custom shaders on its own.
> I believe defining `BEFORE_xx` and `OVERRIDE_xx` and building the main processing logic should be done by editing and crafting it in your preferred text editor.

## Usage

1. Click "Window" -> "koturn" -> "lilToon Custom Generator".
2. Edit items on "lilToon Custom Generator" window.
3. Click "Generate Custom Shader" button at the bottom of the window.

## System requirements

- Unity 2019.4 and later

## LICENSE

This software is released under the MIT License, see [LICENSE](LICENSE "LICENSE").
