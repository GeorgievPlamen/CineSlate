export const validate = {
  email: function (email: string | null | undefined): boolean {
    if (!email) return false;

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  },
  password: function (password: string | null | undefined): boolean {
    if (!password) return false;

    const passwordRegex =
      /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,30}$/;
    return passwordRegex.test(password);
  },
};
