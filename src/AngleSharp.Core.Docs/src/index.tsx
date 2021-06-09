import { PiletApi } from 'piral-docs-tools';

const createDoclet = require('piral-docs-tools/doclet');

export function setup(api: PiletApi) {
  createDoclet(api);
}
