---
title: "Roadmap and Ecosystem"
section: "AngleSharp.Core"
---
# Roadmap and Ecosystem

This page replaces the older time-based "upcoming features" notes. Those notes were tied to pre-1.0 milestones and no longer describe the current project accurately.

## Core Scope

AngleSharp.Core is responsible for the standards-driven foundations:

- HTML parsing and tree construction
- The DOM implementation used across AngleSharp projects
- CSS selector parsing and matching for `querySelector` APIs
- Browsing contexts, configuration, and service composition
- URL, text-source, and related infrastructure

Features that require a full CSSOM, JavaScript runtime integration, XML-specific parsing, rendering, or specialized tooling live in companion repositories.

## Extension Model

AngleSharp is built around `IConfiguration` and `IBrowsingContext`. Services are registered with the configuration and materialized per browsing context, which keeps the core package small while allowing richer capabilities to be added on demand.

Typical examples include:

- `AngleSharp.Css` for full CSS parsing and styling services
- `AngleSharp.Js` for JavaScript integration
- `AngleSharp.Xml` for XML and XHTML workflows
- Custom loaders, requesters, selector factories, or element construction services

## AngleSharp Projects

The following repositories make up the broader AngleSharp ecosystem:

| Project | Purpose | Repository |
|---|---|---|
| AngleSharp.Css | Full CSS parser, CSSOM, and styling services | https://github.com/AngleSharp/AngleSharp.Css |
| AngleSharp.Js | JavaScript integration for browsing contexts | https://github.com/AngleSharp/AngleSharp.Js |
| AngleSharp.Wasm | WebAssembly-oriented integration work for AngleSharp | https://github.com/AngleSharp/AngleSharp.Wasm |
| AngleSharp.Xml | XML, XHTML, and related XML-oriented parsing support | https://github.com/AngleSharp/AngleSharp.Xml |
| AngleSharp.Renderer | Rendering-focused companion project | https://github.com/AngleSharp/AngleSharp.Renderer |
| AngleSharp.Diffing | DOM and markup diffing utilities | https://github.com/AngleSharp/AngleSharp.Diffing |
| AngleSharp.XPath | XPath support on top of the AngleSharp DOM | https://github.com/AngleSharp/AngleSharp.XPath |

## Where To Look For Current Status

Avoid reading old roadmap language as a promise of current support. For up-to-date information use:

- `README.md` for package scope and positioning
- `CHANGELOG.md` for released changes
- The test suite for executable behavior
- GitHub issues and pull requests for ongoing work
