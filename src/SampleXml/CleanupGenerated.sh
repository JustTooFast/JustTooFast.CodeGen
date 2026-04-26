#!/usr/bin/env bash
set -euo pipefail

project_dir="${1:?project directory is required}"

input_dir="${project_dir}/Input"
output_dir="${project_dir}/Output"

if [ ! -d "$output_dir" ]; then
    exit 0
fi

prefixes=$(
    find "$input_dir" -maxdepth 1 -type f -printf '%f\n' \
    | sed 's/\.[^.]*$//'
)

find "$output_dir" -type f -name "*.gen.cs" | while IFS= read -r file; do
    name="$(basename "$file")"
    stem="${name%.gen.cs}"
    keep=0

    while IFS= read -r prefix; do
        [ -z "$prefix" ] && continue

        if [ "$stem" = "${prefix}Emitter" ] \
            || [ "$stem" = "${prefix}Builder" ] \
            || [ "$stem" = "${prefix}Model" ]; then
            keep=1
            break
        fi
    done <<< "$prefixes"

    if [ "$keep" -eq 0 ]; then
        rm -f "$file"
    fi
done
