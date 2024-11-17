export enum userErrors {
  InvalidEmail = "That doesn't look like an email address.",
  InvalidFirstName = 'First Name must be below 30 chars.',
  InvalidLastName = 'Last Name must be below 30 chars.',
  MissingFirstName = 'Please enter your first name.',
  MissingLastName = 'Please enter your last name.',
  MissingPassword = "Can't login without a password.",
  InvalidPassword = 'Password must include 8-30 chars, with uppercase, lowercase, digit, and special symbol.',
  PasswordsMatch = "Passwords don't match.",
  NotFound = 'User with this email not found or password is wrong.',
  EmailInUse = 'There is a user already registered with this email.',
}
