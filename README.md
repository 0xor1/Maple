Maple
=====
[![Build Status](https://github.com/0xor1/maple/actions/workflows/build.yml/badge.svg)](https://github.com/0xor1/maple/actions/workflows/build.yml)
[![Coverage Status](https://coveralls.io/repos/github/0xor1/maple/badge.svg)](https://coveralls.io/github/0xor1/maple)
[![Demo Live](https://img.shields.io/badge/demo-live-4ec820)](https://maple.dans-demos.com)

A simple HR management tool, to track skill proficiencies and profiles.

### Prerequisites

To build and run this project you need `.net core 8`, `docker` and `docker-compose` installed.

To build and run unit tests:
```bash
./bin/pre
```
To build and run the app:
```bash
./bin/run
```
You can pass parameter `nuke` to either `./bin/pre` or `./bin/run` to delete
docker containers and rebuild them, this is typically useful if there has been a db schema change.