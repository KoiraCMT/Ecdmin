export const alphaDash = (msg) => {
  return { pattern: /^[a-zA-Z][-.-_a-zA-Z0-9]+$/, message: msg }
}

export const requiredAndBetween = (min, max) => {
  return [
    { required: true },
    { min: min, max: max }
  ]
}
